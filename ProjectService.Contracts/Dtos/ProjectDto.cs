﻿namespace ProjectService.Contracts.Dtos;

public class ProjectDto
{
    public string Title { get; init; }
    public string Album { get; init; }
    public string Description { get; init; }
    public string? ReleaseInUtc { get; init; }
    public float BeatsPerMinute { get; init; }
    public MusicalProfileDto? MusicalProfile { get; init; }
}
