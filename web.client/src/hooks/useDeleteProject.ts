import { HttpStatusCode } from "axios";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { userActions } from "redux/features/user/userSlice";
import { RootState } from "redux/store";
import { DeleteProjectResponse } from "services/project/contracts";
import { deleteProject } from "services/project/projectServiceClient";

export const useDeleteProject = (): [
  create: (id: string) => Promise<void>,
  response?: DeleteProjectResponse
] => {
  const user = useSelector((state: RootState) => state.user);
  const [response, setResponse] = useState<DeleteProjectResponse>();
  const dispatch = useDispatch();

  const inner = async (id: string) => {
    const result = await deleteProject({
      token: user.token,
      id: id,
    });

    setResponse(result);

    if (
      result.errors &&
      result.errors[0].code === HttpStatusCode.Unauthorized
    ) {
      dispatch(userActions.logout());
    }
  };

  return [inner, response];
};
