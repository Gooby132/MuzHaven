import React from "react";

type Props = {
  onChange: (key: number) => void;
};

export const KeyPicker = ({ onChange }: Props) => {
  return (
    <div>
      <select onChange={(e) => onChange(parseInt(e.target.value))}>
        <option value={0}>C</option>
        <option value={1}>CSharp_DFlat</option>
        <option value={2}>D</option>
        <option value={3}>DSharp_EFlat</option>
        <option value={4}>E</option>
        <option value={5}>F</option>
        <option value={6}>FSharp_GFlat</option>
        <option value={7}>G</option>
        <option value={8}>GSharp_AFlat</option>
        <option value={9}>A</option>
        <option value={10}>ASharp_BFlat</option>
        <option value={11}>B</option>
      </select>
    </div>
  );
};
