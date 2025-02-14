using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcSquads.Radios
{
	public interface IAudioClip
	{
		/// <summary>
		/// The duration of the audio clip.
		/// </summary>
		float Duration { get; }

		/// <summary>
		/// Plays the audio clip.
		/// </summary>
		/// <returns>A task that completes once the audio is stopped.</returns>
		Task Play();

		/// <summary>
		/// Cancels the audio clip's playback.
		/// </summary>
		void Cancel();

		/// <summary>
		/// Invoked when the audio clip gets cancelled.
		/// </summary>
		event EventHandler? Cancelled;
	}
}
