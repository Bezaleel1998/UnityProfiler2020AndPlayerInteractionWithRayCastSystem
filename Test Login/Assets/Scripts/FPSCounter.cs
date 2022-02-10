using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public int avgFrameRate;
    public Text display_Text;

    ProfilerRecorder _systemMemoryRecorder;
    ProfilerRecorder _gcMemoryRecorder;
    ProfilerRecorder _mainThreadTimeRecorder;


    public void Update()
    {

        FPSCurrentPerformance();
        OSDSystemMonitor();

    }

    #region FPSOSDSystemMonitor

    void FPSCurrentPerformance()
    {

        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;

        display_Text.text = "<color=lime>" + avgFrameRate.ToString() + " FPS </color>";

    }

    #endregion

    #region OtherOSDSystemMonitor

    double GetRecorderFrameAverage(ProfilerRecorder recorder)
    {
        var samplesCount = recorder.Capacity;
        if (samplesCount == 0)
            return 0;

        double r = 0;
        unsafe
        {
            var samples = stackalloc ProfilerRecorderSample[samplesCount];
            recorder.CopyTo(samples, samplesCount);
            for (var i = 0; i < samplesCount; ++i)
                r += samples[i].Value;
            r /= samplesCount;
        }

        return r;
    }

    void OnEnable()
    {
        _systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
        _gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        _mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Main Thread", 15);
    }

    void OnDisable()
    {
        _systemMemoryRecorder.Dispose();
        _gcMemoryRecorder.Dispose();
        _mainThreadTimeRecorder.Dispose();
    }

    void OSDSystemMonitor()
    {

        display_Text.text += "\r\n <color=red>" + $"Frame Time: {GetRecorderFrameAverage(_mainThreadTimeRecorder) * (1e-6f):F1} ms </color>";
        display_Text.text += "\r\n <color=yellow>" + $"GC Memory: {_gcMemoryRecorder.LastValue / (1024 * 1024)} MB </color>";
        display_Text.text += "\r\n <color=green>" + $"System Memory: {_systemMemoryRecorder.LastValue / (1024 * 1024)} MB </color>";

    }

    #endregion
}
