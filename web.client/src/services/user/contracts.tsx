import { ErrorDto } from "services/commons/contracts";

export const PASSWORD_GROUP_CODE = 1;
export const EMAIL_GROUP_CODE = 2;
export const FIRST_NAME_GROUP_CODE = 3;
export const LAST_NAME_GROUP_CODE = 4;
export const BIO_GROUP_CODE = 5;
export const STAGE_NAME_GROUP_CODE = 6;

// create project request

export type CreateProjectRequest = {
  token?: string
  project: ProjectDto
}

export type CreateProjectResponse = {
  isError: boolean,
  project?: CompleteProjectDto
  errors?: ErrorDto[]
}

export type RegisterRequest = {
  email: string;
  password: string;
  stageName: string;
  firstName: string;
  lastName: string;
  bio: string;
};

export type LoginRequest = {
  email: string;
  password: string;
};

export type User = {
  id: string;
  bio: string;
  email: string;
  firstName: string;
  lastName: string;
  stageName: string;
};

export type RegisterResponse = {
  result?: AuthorizedUserDto;
  errors?: ErrorDto[];
  isError: boolean;
};

export type LoginResponse = {
  result?: AuthorizedUserDto;
  errors?: ErrorDto[];
  isError: boolean;
};

export type AuthorizedUserDto = {
  user: User;
  token: string;
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