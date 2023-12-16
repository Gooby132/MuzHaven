import { Stem } from "components/organizem/stem/Stem";
import React from "react";
import { CompleteStemDto } from "services/stem/contracts";
import styled from "styled-components";

const Container = styled.div`
.stem-container{
  margin-top: 1.5em;
}`;

type Props = {
  stems?: CompleteStemDto[];
};

export const StemsLayout = ({ stems }: Props) => {
  if (stems === undefined) return <p>Upload new stems</p>;

  return (
    <Container>
      {stems.map((stem) => (
        <div className="stem-container" key={stem.id}>
          <Stem key={stem.id} stem={stem} />
        </div>
      ))}
    </Container>
  );
};
