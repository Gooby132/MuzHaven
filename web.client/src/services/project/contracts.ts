export type CreateProjectRequest = {
  token?: string
  project: ProjectDto
}

export type CreateProjectResponse = {
  isError: boolean,
  project?: CompleteProjectDto
  errors?: ErrorBaseDto[]
}

// dtos
export type ErrorBaseDto = {
  code: number;
  group: number;
  message?: string;
};

export type MusicalProfileDto  = {
  key?: number
  scale?: number
}

export type ProjectDto = {
  title: string;
  album: string;
  description?: string;
  releaseInUtc?: string;
  beatsPerMinute?: number;
  musicalProfile?: MusicalProfileDto;
}

export interface CompleteProjectDto extends ProjectDto {
  id: string
  createdInUtc: string;
}


