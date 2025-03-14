﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Constants;
public class MPFileBusinessMessages
{
    public const string SectionName = "MPFile";

    public const string MPFileNotExists = "MPFileNotExists";
    public const string FileIsNotImageFile = "FileIsNotImageFile";
    public const string FilesCountMusBeBetweenTwoAndSix = "FilesCountMusBeBetweenTwoAndSix";
    public const string ImageOverload = "ImageOverload";
    public const string FileIsIncorrect = "FileIsIncorrect";
    public const string FileIsNotVideoFile = "FileIsNotVideoFile";
    public const string CollectionVideoFileSizeIsOver = "CollectionVideoFileSizeIsOver";
    public const string VideoFileIsIncorrect = "VideoFileIsIncorrect";
}
