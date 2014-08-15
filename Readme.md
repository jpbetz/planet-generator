Procedural Planet Generator
===========================

A random planet generator.  Planetary terrain is generated and rendered onto
3D planet models that can be viewed from space at any angle.

3D perlin noise is used to produce the seamless planetary terrain.  A sphere from the 3D
noise is  mapped onto 2D textures and then wrapped back onto a 3d sphere model of the planet
for rendering.  This allows for high resolution terrain while still keeping the vertex count
on the model relatively low.

Screenshots
-----------
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/blueplanet.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/blueplanet2.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/greenplanet.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/iceplanet.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/iceplanet2.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/marbleplanet.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/orangeplanet.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/purpleplanet.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/redplanet.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/whiteplanet.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/yellowplanet.png">
<img width="80" height="80" src="https://raw.githubusercontent.com/jpbetz/planet-generator/master/Assets/Screenshots/yellowplanet2.png">

Requirements
------------
* Unity 4.6+

Features
--------
* 3D rendered generated planets, Fully lit and shaded
* Fast.  Only takes about 1 sec to generate and render a new planet on an iPhone
* OSX, iOS and Android support
* Zoom and rotate controls (pinch to zoom on mobile)
* 3D scene includes System Star and surrounding galaxy
* Save list to persist favorite discovered planets

What is generated?
------------------
* Diffuse and specular texture maps for planetary terrain and oceans
* Terrain height (part of 3D planet mesh, and aligned with the texture maps)
* Color of terrain and ocean, using generated gradations for topology
* System star light and intensity
* Ice effects based on planet temperature (ice has boosted specularity to give proper visual effect)
* Generate planet names as well, just for fun

TODO
----
[ ] Generate next planet(s) in background to minimize wait
[ ] Progressively generate higher resolution textures in background (after next planets have been generated)
[ ] Zoom out to solar system scale,  draw orbit lines
[ ] Atmosphere generation
[ ] Physics based Star and Planet Mass, orbit, light and temperature values
[ ] Add support for moons (both as primary planet and as planet primary orbits around)
[ ] Level of detail renderer (LOD), allowing for memory efficient zoom from deep space to human scale on planet surface with high resolution throughout.
[ ] Biome generation
[ ] Surface feature generation:  Plants, Concave terrain features (arches, caves, overhung cliffs),  erosion, tectonics
