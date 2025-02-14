using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcSquads.Radios
{
	public interface IMemberRadio
	{
		/// <summary>
		/// The <see cref="ISquadRadio"/> this <see cref="IMemberRadio"/> is connected to.
		/// </summary>
		ISquadRadio SquadRadio { get; }

		/// <summary>
		/// The <see cref="IRadioSpeech"/>s that are waiting for the <see cref="SquadRadio"/>
		/// to select them to be played.
		/// </summary>
		IReadOnlyList<IRadioSpeech> QueuedSpeechs { get; }


		/// <summary>
		/// Says something via the <see cref="SquadRadio"/>.
		/// </summary>
		/// <param name="speech"></param>
		/// <returns>A task that completes once the speech stops playing.</returns>
		Task Say(IRadioSpeech speech);

		/// <summary>
		/// Reserves space in the <see cref="SquadRadio"/>, so <see cref="IRadioSpeech"/>s with lower
		/// priority intents than the given <paramref name="intent"/> won't be played for the specified 
		/// <paramref name="time"/>.<br/>
		/// This way the <see cref="SquadRadio"/> won't be bloated with noise during an important
		/// <see cref="IRadioSpeech"/> that will be given later.
		/// </summary>
		/// <param name="time">The time the reservation will last.</param>
		/// <param name="intent">The intent of the reservation.</param>
		void ReserveSpeech(TimeSpan time, Enum intent);
	}
}
