using Microsoft.Xna.Framework;
using System;

namespace ChaiFoxes.FMODAudio
{
  interface IGeometry : IDisposable, IUserData
  {

    // Polygon manipulation.
    FmodResult AddPolygon(float directocclusion, float reverbocclusion, bool doublesided, int numvertices, Vector3[] vertices, out int polygonindex);

    FmodResult GetNumPolygons(out int numpolygons);

    FmodResult GetMaxPolygons(out int maxpolygons, out int maxvertices);

    FmodResult GetPolygonNumVertices(int index, out int numvertices);

    FmodResult SetPolygonVertex(int index, int vertexindex, ref Vector3 vertex);

    FmodResult GetPolygonVertex(int index, int vertexindex, out Vector3 vertex);

    FmodResult SetPolygonAttributes(int index, float directocclusion, float reverbocclusion, bool doublesided);

    FmodResult GetPolygonAttributes(int index, out float directocclusion, out float reverbocclusion, out bool doublesided);


    // Object manipulation.

    bool Active { get; set; }

    FmodResult SetRotation(ref Vector3 forward, ref Vector3 up);

    FmodResult GetRotation(out Vector3 forward, out Vector3 up);

    Vector3 Position { get; set; }
    Vector3 Scale { get; set; }

    FmodResult Save(IntPtr data, out int datasize);

  }
}
