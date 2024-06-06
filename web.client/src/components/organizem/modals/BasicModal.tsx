import React, { PropsWithChildren } from "react"
import styled, { useTheme } from "styled-components"
// @ts-ignore
import Modal from 'react-modal'

export interface Props {
  isOpen: boolean
}

const BasicModal = (props : PropsWithChildren<Props>) => {
  const theme = useTheme();

  return(
    <Modal 
      isOpen={props.isOpen}
      style={{
        overlay: {
          background: theme.primary
        },
        content: {
          background: theme.secondary,
          color: theme.text
        }
      }}
    >
      {props.children}
    </Modal>
  )
}
export default BasicModal