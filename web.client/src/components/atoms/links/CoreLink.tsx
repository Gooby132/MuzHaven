import React from 'react'
import { Link } from 'react-router-dom'

type Props = {
  text: string;
  to: string;

}

export const CoreLink = (props: Props) => {
  return (
    <Link to={props.to}>{props.text}</Link>
  )
}