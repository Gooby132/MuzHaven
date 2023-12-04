import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { CreateProjectResponse, ProjectDto } from '@services/project/contracts'

export type ProjectState = {
  projects: ProjectDto[]
}

export type MusicalProfile  = {
  key: number
  scale: number
}

const initialState: ProjectState ={
  projects: []
}

export const projectSlice = createSlice({
  name: "project",
  initialState,
  reducers: {
    create: (state, payload: PayloadAction<CreateProjectResponse>) => {
        
    }
  }
})

export const projectReducers = projectSlice.reducer
