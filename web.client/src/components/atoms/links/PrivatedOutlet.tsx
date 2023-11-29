import { ReactElement } from 'react'
import { Route, redirect } from 'react-router-dom';

type Props = {
  children: ReactElement<typeof Route>
  redirectTo: string
  flag: boolean
}

export const PrivatedOutlet = ({children, flag, redirectTo}: Props) => {
  if(!flag){
    console.log("redirect")
    redirect(redirectTo)
  }

  return <>{children}</>
}