import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { LoginResult  } from "../../../services/user/userServiceClient";

export interface UserState {
  loggedIn: boolean,
  token?: string | undefined,
  firstName?: string | undefined,
  lastName?: string | undefined,
  stageName?: string | undefined,
}

const initialState: UserState ={
  loggedIn: false,
  token: undefined,
  firstName: undefined,
  lastName: undefined,
  stageName: undefined,
}

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    login: (state, action: PayloadAction<LoginResult>) => {
      state.token = action.payload.token;
      state.firstName = action.payload.user.firstName;
      state.lastName = action.payload.user.lastName;
      state.stageName = action.payload.user.stageName;
      state.loggedIn = true
    }
  }
})

export const userActions = userSlice.actions
export const userReducers = userSlice.reducer