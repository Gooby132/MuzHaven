// store/configureStore.js
import { configureStore } from '@reduxjs/toolkit';
import counterReducer from '../reducers/counterSlice';

export const store = configureStore({
  reducer: {
    counter: counterReducer,
    // Add more reducers here if needed
  },
});