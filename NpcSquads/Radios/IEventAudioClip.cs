using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcSquads.Radios
{
	public interface IEventAudioClip : IAudioClip
	{

		IReadOnlyList<IAudioClipEvent> Events { get; }


		delegate void AudioClipEventDelegate(IAudioClipEvent audioEvent);
	}
}
