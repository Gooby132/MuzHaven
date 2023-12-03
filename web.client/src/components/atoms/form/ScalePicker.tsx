import React from "react";

type Props = {
  onChange: (scale: number) => void;
};

export const ScalePicker = ({ onChange }: Props) => {
  return (
    <div>
      <select onChange={e => onChange(parseInt(e.target.value))}>
        <option value={0}>Major</option>
        <option value={1}>NaturalMinor</option>
        <option value={2}>HarmonicMinor</option>
        <option value={3}>MelodicMinor</option>
        <option value={4}>Blues</option>
        <option value={5}>PentatonicMajor</option>
        <option value={6}>PentatonicMinor</option>
        <option value={7}>Dorian</option>
        <option value={8}>Mixolydian</option>
        <option value={9}>Phrygian</option>
        <option value={10}>Locrian</option>
        <option value={11}>WholeTone</option>
        <option value={12}>Chromatic</option>
      </select>
    </div>
  );
};
