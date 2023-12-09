import { combineReducers, configureStore } from '@reduxjs/toolkit'
import { userReducers } from './features/user/userSlice'
import { persistStore, persistReducer } from 'redux-persist'
import storage from 'redux-persist/lib/storage' // defaults to localStorage for web
import { projectReducers } from './features/project/projectSlice'

const persistConfig = {
  key: 'root',
  storage,
}


const persistedReducer = persistReducer(persistConfig, userReducers)

const reducers = combineReducers({
  user: persistedReducer,
  project: projectReducers
})
// const persistedReducer = reducers

export const store = configureStore({
  reducer: reducers
})

export const persistor = persistStore(store)

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch
