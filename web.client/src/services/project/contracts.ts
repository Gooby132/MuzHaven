import { ErrorDto } from "services/commons/contracts"

export type CreateProjectRequest = {
  token?: string
  project: ProjectDto
}

export type CreateProjectResponse = {
  isError: boolean,
  project?: CompleteProjectDto
  errors?: ErrorDto[]
}

// dtos
export type MusicalProfileDto  = {
  key?: number
  scale?: number
}

export interface ProjectDto {
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


