using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ChaiFoxes.FMODAudio
{
	/// <summary>
	/// Sound listener in 3D space. Listens to positioned 3D sounds.
	/// 
	/// NOTE: Do not mess with low-level listeners if you're using those.
	/// It *probably* should be fine, but you're doing that at your own risk.
	/// </summary>
	public class Listener3D
	{
		/// <summary>
		/// List of all listeners. Used to keep track of indices.
		/// </summary>
		private static List<Listener3D> _listeners = new List<Listener3D>();

		/// <summary>
		/// Listener index. Used to communicate with low-level API.
		/// </summary>
		private int _index;

		/// <summary>
		/// Listener position in 3D space. Used for panning and attenuation
		/// </summary>
		public Vector3 Position3D
		{
			get
			{
				GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
				return position;
			}
			set
			{
				GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
				SetAttributes(value, velocity, forwardVector, upVector);
			}
		}

		/// <summary>
		/// Listener velocity in 3D space. Used for doppler effect.
		/// </summary>
		public Vector3 Velocity3D
		{
			get
			{
				GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
				return velocity;
			}
			set
			{
				GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
				SetAttributes(position, value, forwardVector, upVector);
			}
		}

		/// <summary>
		/// Forwards orientation, must be of unit length (1.0) and perpendicular to up.
		/// UnitY by default.
		/// </summary>
		public Vector3 ForwardOrientation
		{
			get
			{
				GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
				return forwardVector;
			}
			set
			{
				GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
				SetAttributes(position, velocity, value, upVector);
			}
		}
		
		/// <summary>
		/// Upwards orientation, must be of unit length (1.0) and perpendicular to forward.
		/// UnitZ by default.
		/// </summary>
		public Vector3 UpOrientation
		{
			get
			{
				GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
				return upVector;
			}
			set
			{
				GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
				SetAttributes(position, velocity, forwardVector, value);
			}
		}

		/// <summary>
		/// Gets all listener attributes at once.
		/// </summary>
		public void GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector)
		{
			AudioSystem.FMODSystem.get3DListenerAttributes(
				_index, 
				out FMOD.VECTOR pos, 
				out FMOD.VECTOR vel, 
				out FMOD.VECTOR forward, 
				out FMOD.VECTOR up
			);
			position = pos.ToVector3();
			velocity = vel.ToVector3();
			forwardVector = forward.ToVector3();
			upVector = up.ToVector3();
		}
		
		/// <summary>
		/// Sets all listener attributes at once.
		/// </summary>
		public void SetAttributes(Vector3 position, Vector3 velocity, Vector3 forwardVector, Vector3 upVector)
		{
			var pos = position.ToFmodVector();
			var vel = velocity.ToFmodVector();
			var forw = forwardVector.ToFmodVector();
			var up = upVector.ToFmodVector();
			AudioSystem.FMODSystem.set3DListenerAttributes(
				_index,
				ref pos,
				ref vel, 
				ref forw, 
				ref up
			);
		}
		private Listener3D()
		{
			_index = _listeners.Count;
			_listeners.Add(this);
			AudioSystem.FMODSystem.set3DNumListeners(_listeners.Count);

			SetAttributes(Vector3.Zero, Vector3.Zero, Vector3.UnitY, Vector3.UnitZ);
		}

		/// <summary>
		/// Creates a new listener.
		/// </summary>
		public static Listener3D Create()
		{
			return new Listener3D();
		}

		/// <summary>
		/// Destroys the listener.
		/// </summary>
		public void Destroy()
		{
			if (_index != _listeners.Count - 1)
			{
				// Listener's index is its only link to its low-level attributes.
				// We don't have access to actual low-level listener objects -
				// if they even exist - and have to resort to this.
				// Basically, if user decides to destroy a listener from the middle of
				// listener list, we take the very last listener and place it instead
				// of the destoyed listener.

				var last = _listeners[_listeners.Count - 1];

				// Saving attribute.
				last.GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forwardVector, out Vector3 upVector);
				// Replacing index.
				last._index = _index;
				// Copying attributes over.
				last.SetAttributes(position, velocity, forwardVector, upVector);
				
				// Changing last element's place in listener list.
				_listeners.RemoveAt(_listeners.Count - 1);
				_listeners.Insert(last._index, last);
			}

			_listeners.Remove(this);
			AudioSystem.FMODSystem.set3DNumListeners(_listeners.Count);

			_index = -1;
		}
	}
}
