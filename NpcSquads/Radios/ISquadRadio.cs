using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcSquads.Radios
{
	public interface ISquadRadio
	{
		/// <summary>
		/// The <see cref="IMemberRadio"/>s that are part of the squad.
		/// </summary>
		IReadOnlyList<IMemberRadio> MemberRadios { get; }

		/// <summary>
		/// The radios that are currently making sound.
		/// </summary>
		IReadOnlyList<IMemberRadio> SpeakingRadios { get; }

		/// <summary>
		/// The radios that reserved time for them to speak later without being
		/// interrumpted by speechs of lower hierarchy intent that the one reserved.
		/// </summary>
		IReadOnlyList<IMemberRadio> ReservedSpeechRadios { get; }

		/// <summary>
		/// The radios whose speech are reserved or that are currently making sound.
		/// </summary>
		IReadOnlyList<IMemberRadio> ActiveRadios { get; }

		/// <summary>
		/// The radio intent that has the most priority from the <see cref="ActiveRadios"/>.
		/// </summary>
		Enum? MostPriorityRadioIntent { get; }


		/// <summary>
		/// Selects a <see cref="IRadioSpeech"/> to play next based on their intent priority
		/// and wheter they're a response to other speechs.
		/// </summary>
		/// <returns>A task that completes once the selected <see cref="IRadioSpeech"/> finishes playing.</returns>
		Task PlayNextMemberSpeech();


		/// <summary>
		/// Invoked when a <see cref="IRadioSpeech"/> starts playing.
		/// </summary>
		event SpeechDelegate SpeechStarted;

		/// <summary>
		/// Invoked when a <see cref="IRadioSpeech"/> plays succesfully
		/// </summary>
		event SpeechDelegate SpeechCompleted;

		/// <summary>
		/// Invoked when a <see cref="IRadioSpeech"/> playback gets cancelled.
		/// </summary>
		event SpeechDelegate SpeechCancelled;

		/// <summary>
		/// Invoked when a <see cref="IAudioClipEvent"/> gets marked as started.
		/// </summary>
		event SpeechAudioEventDelegate SpeechEventStarted;

		/// <summary>
		/// Invoked when a <see cref="IAudioClipEvent"/> gets marked as completed.
		/// </summary>
		event SpeechAudioEventDelegate SpeechEventCompleted;

		/// <summary>
		/// Invoked when a <see cref="IAudioClipEvent"/> gets cancelled.
		/// </summary>
		event SpeechAudioEventDelegate SpeechEventCancelled;


		delegate void SpeechDelegate(IRadioSpeech speech);
		delegate void SpeechAudioEventDelegate(IAudioClipEvent audioEvent, IRadioSpeech speech, IMemberRadio memberRadio);
	}
}
