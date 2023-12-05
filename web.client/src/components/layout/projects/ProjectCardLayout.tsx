import { ReactNode } from "react";
import styled from "styled-components";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  >.header{
    font-size:1.1em;
  }
  >.body{

  }
  >.footer-container{
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    font-size:0.8em;
  }
`;

type Props = {
  header: ReactNode;
  body: ReactNode;
  footer: ReactNode;
  footerNotes?: ReactNode;
};

export const ProjectCardLayout = ({
  header,
  body,
  footer,
  footerNotes,
}: Props) => {
  return (
    <Container>
      <div className="header">{header}</div>
      <div className="body">{body}</div>
      <div className="footer-container">
        <div className="footer">{footer}</div>
        <div className="footer-notes">{footerNotes}</div>
      </div>
    </Container>
  );
};
