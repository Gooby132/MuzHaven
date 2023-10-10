// store/configureStore.js
import { configureStore } from '@reduxjs/toolkit';
import tokenReducer from '../reducers/tokenSlice';

export const store = configureStore({
  reducer: {
    tokenState: tokenReducer,
  },
});