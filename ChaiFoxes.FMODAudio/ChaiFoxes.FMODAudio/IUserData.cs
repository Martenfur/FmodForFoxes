using System;

namespace ChaiFoxes.FMODAudio
{
  public interface IUserData
	{
    // Userdata set/get.
    IntPtr Userdata { get; set; }

    IntPtr Handle { get; set; }

    bool HasHandle();

    void ClearHandle();
  }
}
