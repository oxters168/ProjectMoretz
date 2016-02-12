﻿//using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxMath;

public struct dtexdata_t
{
    public Vector3 reflectivity; // RGB reflectivity
    public int nameStringTableID; // index into TexdataStringTable
    public int width, height; // source image
    public int view_width, view_height;
}