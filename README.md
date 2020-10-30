# Swisstopo_Unity
Data from Swisstopo into Unity

## DEM
Download: https://trac.osgeo.org/osgeo4w/
```bash
gdalinfo map.tif -stats
gdal_translate -ot UInt16 -scale 0 5000 0 65535 -outsize 2048 2048 -of ENVI D:\in.tif D:\out.raw
```
Height (5000) has to match with the Unity import Height of the terrain.
