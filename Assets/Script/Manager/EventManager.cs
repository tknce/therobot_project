using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventWave : int
{
    Eva_balon, Stage1_clear, Eva_play, Hans_meeting, Hans_accept, Hans_refuse, Hans_success ,Stage2_enter,
}
public struct Event_condition
{
    public bool balon;
    public bool jetpack;
}
public class EventManager : Singleton<EventManager>
{
    EventWave eventWave;
    public Event_condition event_conditon = new Event_condition();
    

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetWave(EventWave _wave)
    {
        switch (eventWave)
        {
            case EventWave.Eva_balon:
                // Eva_balon 贸府 内靛
                if(!event_conditon.balon)
                {
                    _wave = EventWave.Eva_balon;
                    return;
                }                    
                break;
            case EventWave.Stage1_clear:
                // Stage1_clear 贸府 内靛
                break;
            case EventWave.Eva_play:
                // Eva_play 贸府 内靛
                break;
            case EventWave.Hans_meeting:
                // Hans_meeting 贸府 内靛
                break;
            case EventWave.Hans_accept:
                // Hans_accept 贸府 内靛
                break;
            case EventWave.Hans_refuse:
                // Hans_refuse 贸府 内靛
                break;
            case EventWave.Hans_success:
                // Hans_success 贸府 内靛
                break;
            case EventWave.Stage2_enter:
                // Stage2_enter 贸府 内靛
                break;
            default:
                // 扁夯 贸府 内靛 (鞘夸俊 蝶扼)
                break;
        }
        eventWave = _wave;
    }

    public EventWave GetWave()
    {
        return eventWave;
    }

    public void SetEvent_condition(Event_condition _confition)
    {
        event_conditon = _confition;
    }

}
