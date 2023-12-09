import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { FetchProjectsResponse } from 'services/project/contracts'
import { CompleteProjectDto } from 'services/user/contracts'

export type ProjectState = {
  projects: CompleteProjectDto[]
}

export type MusicalProfile  = {
  key: number
  scale: number
}

const initialState: ProjectState = {
  projects: []
}

export const projectSlice = createSlice({
  name: "project",
  initialState,
  reducers: {
    fetchProjects: (state, payload: PayloadAction<FetchProjectsResponse>) => {
        state.projects = payload.payload.result!.projects
    }
  }
})

export const projectReducers = projectSlice.reducer
export const projectActions = projectSlice.actions
