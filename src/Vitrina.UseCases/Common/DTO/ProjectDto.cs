﻿using Vitrina.Domain.Project;

namespace Vitrina.UseCases.Common.DTO;

/// <summary>
/// Project dto.
/// </summary>
public class ProjectDto
{
    /// <summary>
    /// Project id.
    /// </summary>
    required public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    required public string Name { get; init; }

    /// <summary>
    /// Project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Project aim.
    /// </summary>
    public string? Aim { get; init; }

    /// <summary>
    /// Project client.
    /// </summary>
    public string? Client { get; init; }

    /// <summary>
    /// Markdown of page.
    /// </summary>
    public string? Markdown { get; init; }

    /// <summary>
    /// Url of project video.
    /// </summary>
    public string? VideoUrl { get; init; }

    /// <summary>
    /// Priority of project.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Path to preview image.
    /// </summary>
    public string? PreviewImagePath { get; set; }

    /// <summary>
    /// Period.
    /// </summary>
    required public string Period { get; set; }

    /// <summary>
    /// Semester.
    /// </summary>
    public SemesterEnum Semester { get; set; }

    /// <summary>
    /// Project content.
    /// </summary>
    public ICollection<ContentDto> Contents { get; init; } = new List<ContentDto>();

    /// <summary>
    /// Project tags.
    /// </summary>
    public ICollection<TagDto> Tags { get; init; } = new List<TagDto>();

    /// <summary>
    /// Project team.
    /// </summary>
    public ICollection<UserDto> Users { get; init; } = new List<UserDto>();

    /// <summary>
    /// Problem of project.
    /// </summary>
    public string? Problem { get; init; }

    /// <summary>
    /// Idea of project.
    /// </summary>
    public string? Idea { get; init; }

    /// <summary>
    /// Problem of project.
    /// </summary>
    public string? Solution { get; init; }

    /// <summary>
    /// List of custom blocks.
    /// </summary>
    public ICollection<BlockDto> CustomBlocks { get; init; } = new List<BlockDto>();

    /// <summary>
    /// Project sphere.
    /// </summary>
    public string? Sphere { get; init; }

    /// <summary>
    /// Project type.
    /// </summary>
    public string? Type { get; init; }
}
