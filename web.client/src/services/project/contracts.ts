import { ErrorDto } from "services/commons/contracts"

export type DeleteProjectRequest = {
  id: string,
  token?: string
}

export type DeleteProjectResponse = {
  isError: boolean,
  errors?: ErrorDto[];
}

export type FetchProjectByIdRequest = {
  id: string,
  token: string
}

export type FetchProjectByIdResponse = {
  isError: boolean,
  errors?: ErrorDto[];
  result?: CompleteProjectDto; 
}

export type FetchProjectsResponse = {
  result?: FetchProjectResult;
  errors?: ErrorDto[];
  isError: boolean;
};

export type FetchProjectResult = {
  projects: CompleteProjectDto[];
};

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


