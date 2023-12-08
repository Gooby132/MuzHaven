import { useState } from "react";
import styled from "styled-components";
import { ScalePicker } from "./ScalePicker";
import { KeyPicker } from "./KeyPicker";

const Container = styled.div`
  display: flex;
`;

export type MusicProfile = {
  scale?: number;
  key?: number;
};

type Props = {
  onChange: (profile: MusicProfile) => void;
};

export const MusicProfileInput = ({ onChange }: Props) => {
  const [profile, setProfile] = useState<MusicProfile>({
    key: undefined,
    scale: undefined,
  });

  const innerChange = () => {
    onChange(profile);
  };

  return (
    <Container>
      <KeyPicker
        onChange={(val) => {
          setProfile((prev) => {
            return {
              ...prev,
              key: val,
            };
          });

        }}
      />
      <ScalePicker
        onChange={(val) => {
          setProfile((prev) => {
            return {
              ...prev,
              scale: val,
            };
          });
        }}
      />
    </Container>
  );
};
