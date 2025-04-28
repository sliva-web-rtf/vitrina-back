﻿using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project;

/// <summary>
///     Project content.
/// </summary>
public class Content
{
    /// <summary>
    ///     Content id.
    /// </summary>
    [Key]
    public int Id { get; private set; }

    /// <summary>
    ///     Image url.
    /// </summary>
    required public string ImageUrl { get; set; }

    /// <summary>
    ///     Project id.
    /// </summary>
    public int ProjectId { get; private set; }

    /// <summary>
    ///     Project.
    /// </summary>
    required public virtual Project Project { get; set; }
}
