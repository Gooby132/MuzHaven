import { useDispatch, useSelector } from "react-redux";
import { RootState } from "redux/store";
import { fetchProjects } from "services/project/projectServiceClient";
import { projectActions } from "redux/features/project/projectSlice";
import { useState } from "react";
import { FetchProjectsResponse } from "services/project/contracts";

export const useFetchCreatorProjects = () : [
  () => Promise<void>,
  FetchProjectsResponse? 
] => {
  const [response, setResponse] = useState<FetchProjectsResponse>()
  const dispatch = useDispatch();
  const user = useSelector((state: RootState) => state.user);

  const fetchCreatorProjects = async () => {
    if (user.token === undefined) return;

    const projects = await fetchProjects({
      token: user.token,
    });

    setResponse(projects);

    if (!projects.isError) {
      dispatch(projectActions.fetchProjects(projects));
    }
  };

  return [fetchCreatorProjects, response]
}