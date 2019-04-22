# ChaiFoxes.FMODAudio

*I suffer so you won't have to.*

**NOTE: CURRENTLY THIS LIBRARY IS IN DEVELOPMENT AND NUGET PACKAGES ARE NOT AVAILABLE**

The time has come. You're finally witnessing a high-level cross-platform C# library, 
which makes Monogame and FMOD best friends.

![header](/pics/ebites.png)


In case you came here and the only thing you got so far was that hilarious doge meme, 
[FMOD](http://fmod.org) is quite powerful cross-platform audio engine, which is
pretty much the only hope to have any decent audio in Monogame. 

The catch is - FMOD is written in C++, and the only thing C# overlords have
is a bare-bones C# wrapper. No tutorials, no setup guides. Only you and DllImport.

![send help](/pics/help.png)


This is why this library exists. It does the tough part for you, and provides 
basic high-level interface. 

Also note that even though the primary target of this library is Monogame, its sources
will be very useful if you want to use FMOD in some other C#-based project.

## Setup

Setting things up is a little fiddly. Here's the thing - the FMOD license prohibits 
me from distributing their libraries in my Nuget package - so you have to
download them yourself.

### Preparations.

Go to the [FMOD Download page](https://www.fmod.com/download) (accessing it requires
registration), find the FMOD Studio API downloads and download APIs for Windows, Linux
and Android. If you're going to set up all three, of course.


Windows API requires installation, Linux and Android doesn't. You can drop them near
Windows API just to have everything in one place.

![setup1](/pics/setup1.png)

### Windows & Linux

1. Open your DesktopGL or SharpDX Monogame project.
2. Install NuGet package ChaiFoxes.FMODAudio.Desktop. Alternatively, you can compile
it from sources.
3. Navigate to your FMOD Windows API installation. From it navigate to `\api\core\lib`.
You will see two directories: `x64` and `x86`. Each will contain this:

![setup2](/pics/setup2.png)


Out of all the files you'll need only `fmod.dll` from both `x86` and `x64` directories.
Your Monogame project should already have `x86` and `x64` directories 
(if it doesn't - just create them). Copy corresponding versions of `fmod.dll` 
into them. You should end up with it looking like this:


![setup3](/pics/setup3.png)

Make sure dll files will be copied to the output directory:

![setup4](/pics/setup4.png)

4. Navigate to your FMOD Linux API installation. From it navigate to `\api\core\lib`.
This time you will see four directories: 

![setup5](/pics/setup5.png)
 
You'll need only `x86` and `x86_64`.

Each directory contains this:

![setup6](/pics/setup6.png)

You will need only `libfmod` files - those without L. Copy fmod files from `x86`
directory to `x86` directory of your project, and then copy files from `x86_64` 
to your `x64` directory. You'll end up with this:

![setup7](/pics/setup7.png)

*NOTE: If you're going for Linux-only build, you can exclude fmod.dll files.*

Again, make sure all the files you've just added will be copied 
to the output directory:

![setup4](/pics/setup4.png)

And that's it - you got yourself cross-platform desktop FMOD!

### Android

1. Open your Monogame Android project.
2. Install NuGet package ChaiFoxes.FMODAudio.Android. Alternatively, you can compile
it from sources.
3. Create `libs` directory in the root of your project.
4. Navigate to your FMOD Android API installation. From it navigate to `\api\core\lib`.
You will see this:

![setup8](/pics/setup8.png)

5. Each folder contains `libfmod.so` and `libfmodL.so` files. 
You need only `libfmod.so` from each directory. Copy everything over to your `libs`
directory. You'll end up with this:

![setup9](/pics/setup9.png)

6. Select each `.so` file you've just copied, open their Properties and set their
Build Action to `AndroidNativeLibrary`.

![setup10](/pics/setup10.png)

## Playing some tunes!

So, after you've set everything up, it's time to bop some pops, as kids say these days.

1. Find some sound file and import it into Content Pipeline.
2. Select sound file and set its Build Action to Copy. 

![setup11](/pics/setup11.png)

3. Include ChaiFoxes.FMODAudio namespace and paste the following code in your
Initialize() method:
```
AudioMgr.Init("Content/");

var sound = AudioMgr.LoadStreamedSound("test.mp3");
var group = AudioMgr.CreateChannelGroup("group");
AudioMgr.PlaySound(sound, group);
```
4. Compile and hope that you (and me) did everything right.

You can also check out demo project included in sources. Though, note that it requires
main library project to be present and compiled in Debug. You can also replace project reference
with Nuget packages.

## But what about other platforms?

I'd like to make console versions of the library - but currently I have no ability
to do so, and probably won't have for a long time. As for UWP and Apple platforms,
I just don't care about them enough. 

If you want to be a hero and expand the library with any of those platfrorms yourself - 
contact me and we'll figure something out. ; - )


## License and legal stuffs

This library is licensed under MPL 2.0, so you can use it and its code in any 
shenanigans you want. Free games, commercial games, anything - no payment or 
royalties required. Just leave a credit. ; - )

But the show's main star is a bit different. FMOD has its own [license](https://fmod.com/licensing#faq), 
which is much less permissive than mine. 

Demo [music](https://www.youtube.com/watch?v=zZ81qi90E-Y) is provided by Agrofox.

*don't forget to pet your foxes*
