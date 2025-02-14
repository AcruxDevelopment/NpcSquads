using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcSquads.Radios
{
	public interface IRadioSpeech
	{
		/// <summary>
		/// The author of this speech.
		/// </summary>
		IMemberRadio Author { get; }

		/// <summary>
		/// The <see cref="IAudioClip"/>s that are part of this speech.
		/// </summary>
		IReadOnlyList<IAudioClip> AudioClips { get; }

		/// <summary>
		/// The date when this speech will be considered queue-expired.<br/>
		/// Removing it from the queue and preventing it from being played.
		/// </summary>
		DateTime? QueueDeadline { get; }

		/// <summary>
		/// A <see cref="Func{TResult}"/> that will be iterated as long as the speech is in the queue. <br/>
		/// If the function returns <see langword="true"/>, the speech will be considered queue-expired,
		/// therefore removing it from the queue and preventing it from being played.
		/// </summary>
		Func<bool> QueueCancellationFunc { get; }

		/// <summary>
		/// The <see cref="IRadioSpeech"/> this one is responding to.
		/// </summary>
		IRadioSpeech? ResponseTo { get; }

		/// <summary>
		/// The intention of this speech.
		/// </summary>
		Enum Intent { get; }

		/// <summary>
		/// Plays the audios of the radio speech. <br/>
		/// The <see cref="Author"/>'s <see cref="ISquadRadio"/> will not be notified.
		/// </summary>
		/// <returns>A task that completes once the playback stops.</returns>
		Task Play();

		/// <summary>
		/// Cancel the playback of the audios, therefore, the whole speech.
		/// </summary>
		void Cancel();

		/// <summary>
		/// Invoked when the speech gets marked as queue-expired.<br/>
		/// In other words, the speech has been removed from the queue
		/// and was unable to be played to the <see cref="ISquadRadio"/>.
		/// </summary>
		event EventHandler? QueueExpired;

		/// <summary>
		/// Invoked when the speech was cancelled while it was being
		/// played at the <see cref="ISquadRadio"/>.
		/// </summary>
		event EventHandler? MidSentenceCancelled;

		/// <summary>
		/// Invoked when a <see cref="IAudioClipEvent"/> gets marked as started.
		/// </summary>
		event AudioClipEventDelegate? EventStarted;

		/// <summary>
		/// Invoked when a <see cref="IAudioClipEvent"/> gets completed.
		/// </summary>
		event AudioClipEventDelegate? EventCompleted;

		/// <summary>
		/// Invoked when a <see cref="IAudioClipEvent"/> gets cancelled.
		/// </summary>
		event AudioClipEventDelegate? EventCancelled;

		/// <summary>
		/// Invoked when a <see cref="IRadioSpeech"/> responds to this one.
		/// </summary>
		event RadioSpeechDelegate? RespondedBy;

		delegate void AudioClipEventDelegate(IAudioClipEvent audioEvent);
		delegate void RadioSpeechDelegate(IRadioSpeech speech);
	}
}
