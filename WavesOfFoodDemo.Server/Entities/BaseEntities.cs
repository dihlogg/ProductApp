﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WavesOfFoodDemo.Server.Entities;

public class BaseEntities
{
    [Key]
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public Guid? CreateBy { get; set; }

    public Guid? UpdateBy { get; set; }
}