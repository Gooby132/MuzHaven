// reducers/counterSlice.js
import { createSlice } from '@reduxjs/toolkit';

const tokenSlice = createSlice({
  name: 'token',
  initialState: [],
  reducers: {
    addToken: (state, data) => state = data.payload,
  },
});

export const { addToken } = tokenSlice.actions;
export default tokenSlice.reducer;
