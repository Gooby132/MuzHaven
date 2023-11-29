import { combineReducers, configureStore } from '@reduxjs/toolkit'
import { userReducers } from './features/user/userSlice'
import { persistStore, persistReducer } from 'redux-persist'
import storage from 'redux-persist/lib/storage' // defaults to localStorage for web

const persistConfig = {
  key: 'root',
  storage,
}

const reducers = combineReducers({
  user: userReducers
})

// const persistedReducer = persistReducer(persistConfig, reducers)
const persistedReducer = reducers

export const store = configureStore({
  reducer: persistedReducer
})

export const persistor = persistStore(store)

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch
