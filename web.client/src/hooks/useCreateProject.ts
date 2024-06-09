import { HttpStatusCode } from "axios";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { userActions } from "redux/features/user/userSlice";
import { RootState } from "redux/store";
import { CreateProjectResponse, ProjectDto } from "services/user/contracts";
import { createProject } from "services/user/userServiceClient";

export const useCreateProject = () : [
  create: (project: ProjectDto) => Promise<void>,
  response?: CreateProjectResponse
] => {
  const user = useSelector((state: RootState) => state.user);
  const [response, setResponse] = useState<CreateProjectResponse>();
  const dispatch = useDispatch();

  const create = async (project: ProjectDto) => {
    const result = await createProject({
      token: user.token,
      project: project,
    });

    setResponse(result);

    if (
      result.errors &&
      result.errors[0].code === HttpStatusCode.Unauthorized
    ) {
      dispatch(userActions.logout());
    }
  };

  return [create, response];
};
