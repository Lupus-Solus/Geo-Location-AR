# Geo-Location-AR
Test Unity3d ARFoundation / ARCore project to attempt to place gameobjects at real world coordinates. 

TL;DR: Created a very small Geo Location app. Reaching out for a public colab to tighten up the accuracy of gameobject placement. 

Hi All,

At the moment this project is very very simple.  It places a game object at geo coordinates that you hard code into it.  It works, to a certain extent, but it can be a little random with its placing and I’d love it if we could tighten that up.

I scoured the internet for any kind of tutorial that would help with the placing of a Unity game object in the correct location in the real world and everything seemed to be using mapbox.  I then bumped into a project on github that seemed to be doing the Geo mapping VooDoo I needed. (Thanks Togusa09! https://github.com/Togusa09/LocatedAR )

Things to note - 
1. It’s not very elegant at this point, there are no SOLID principles or anything fancy.  This is very rough work in progress.

2.I am reporting to screen the captured gps of the device and the continued gps of the device so you can gauge if more time is needed for calibration.

3. The continued GPS will also help if you want to walk to a particular location and take a screenshot from your device so you can have exact geo coords (I find google maps coords seem to differ from my device coords for some reason)  

4. I am reporting the compass readings to screen - showing the snapshot orientation for north and then live reporting lowest and highest so you can guague if different ways of getting a north reading are more accurate.(place your device on a flat surface for this test).

5. You will have to drill into the Hierarchy: AR Session Origin -> Spawned Objects to get to the cylinder to input your own geo coords

6. I have found that holding my phone flat though calibration seems to calm the ‘jitteryness’ of the compass - this may just be my phone, your results be different.

Please do reach out with any advice / findings / advancements you may have.

Cheers!

Lupus
