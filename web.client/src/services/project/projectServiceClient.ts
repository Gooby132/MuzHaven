import axios from "axios";
import {
  CreateProjectRequest,
  CreateProjectResponse,
  ErrorBaseDto,
  ProjectDto,
} from "./contracts";

const PROJECT_SERVICE_BASE = "http://localhost:8080/api/Projects";
const FETCH_PROJECTS = "/get-projects";
const CREATE_PROJECT = "/create-project";

export const CLIENT_INTERNAL_ERROR = 999;

export type FetchClientProjectsRequest = {
  token: string;
};

export type FetchProjectsResponse = {
  result?: FetchProjectResult;
  errors?: Error[];
  isError: boolean;
};

export type FetchProjectResult = {
  projects: ProjectDto;
};

export const fetchClientsProjects = async ({
  token,
}: FetchClientProjectsRequest): Promise<FetchProjectsResponse> => {
  try {
    const res = await axios.get(`${PROJECT_SERVICE_BASE}${FETCH_PROJECTS}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    return {
      isError: false,
      result: {
        projects: res.data,
      },
    };
  } catch (e: any) {
    switch (e.response.status) {
      case 400:
        return {
          isError: true,
          errors: e.response.data.map(
            (error: { code: number; group: number; message?: string }) => {
              return {
                code: error.code,
                group: error.group,
                message: error.message,
              };
            }
          ),
        };
      default:
        console.log(e.response);
        return {
          isError: true,
        };
    }
  }
};

export const createProject = async ({
  token,
  project,
}: CreateProjectRequest): Promise<CreateProjectResponse> => {
  try {
    if (token === undefined) {
      return {
        isError: true,
        errors: [
          {
            group: CLIENT_INTERNAL_ERROR,
            code: 0,
            message: "token was not provided",
          },
        ],
      };
    }

    const res = await axios.post(
      `${PROJECT_SERVICE_BASE}${CREATE_PROJECT}`,
      {
        project,
        token,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    return {
      isError: false,
      project: {
        id: res.data.id,
        title: res.data.title,
        album: res.data.album,
        createdInUtc: res.data.createdInUtc,
        beatsPerMinute: res.data.beatsPerMinute,
        description: res.data.description,
        releaseInUtc: res.data.releaseInUtc,
        musicalProfile:
          res.data.musicalProfile !== undefined
            ? {
                key: res.data.musicalProfile.key,
                scale: res.data.musicalProfile.scale,
              }
            : undefined,
      },
    };
  } catch (e: any) {
    switch (e.response?.status || 0) {
      case 400:
        return {
          isError: true,
          errors: e.response.data.map((error: ErrorBaseDto) => {
            return {
              code: error.code,
              group: error.group,
              message: error.message,
            };
          }),
        };
      default:
        console.log(e);
        return {
          isError: true,
        };
    }
  }
};
