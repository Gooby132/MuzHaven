import React from "react";
import { SelectInput } from "./SelectInput";

type Props = {
  onChange: (key: number) => void;
};

export const ScalePicker = ({ onChange }: Props) => {
  return (
    <div>
      <SelectInput
        keyNames={[
          [0, "Major"],
          [1, "Natural Minor"],
          [2, "Harmonic Minor"],
          [3, "Melodic Minor"],
          [4, "Blues"],
          [5, "Pentatonic Major"],
          [6, "Pentatonic Minor"],
          [7, "Dorian"],
          [8, "Mixolydian"],
          [9, "Phrygian"],
          [10, "Locrian"],
          [11, "Whole Tone"],
          [12, "Chromatic"],
        ]}
        onChange={onChange}
      />
    </div>
  );
};
