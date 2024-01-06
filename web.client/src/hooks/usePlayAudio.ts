import { useEffect, useState } from "react";
import { getStream } from "services/stem/stemServiceClient";
import { Howl } from "howler";

type Props = {
  id: string;
  format: string;
  url: string;
};

export type Player = {
  play: () => "loading" | "error" | "ok";
  pause: () => void;
  loading: boolean;
  playing: boolean;
};

export const useStemPlayer = ({ id, url, format }: Props): Player => {
  const [playRequest, setPlayRequest] = useState<boolean>(false);
  const [playing, setPlaying] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(false);
  const [player, setPlayer] = useState<Howl | undefined>(undefined);

  useEffect(() => {
    if (playRequest === false) {
      console.log("hook loaded 2");

      return;
    }

    const getStemStream = async () => {
      const stream = await getStream({
        stemId: id,
      });

      if (stream.isError) return;
      console.log("LOADING");

      setPlayer(
        new Howl({
          src: url,
          xhr: {
            method: "GET",
          },
          format: format,
        })
      );
    };

    getStemStream().then(() => {
      setLoading(false);
      console.log("FINISHED");
    });
  }, [playRequest]);

  useEffect(() => {
    if (player === undefined) {
      console.log("use effect loaded on : player");
      return;
    }

    console.log("player is defined requesting play"); // play is loaded requesting initialized play

    play();
  
  }, [player]);
  

  useEffect(() => {
    if (playing) player?.play();

    else player?.pause();

  }, [playing]);

  const play = () => {
    if (!playRequest) { // initial play request
      setPlayRequest(true);
      console.log("REQUEST");
    }

    if (player === undefined) return "loading";

    setPlaying(true);

    return "ok";
  };

  const pause = () => {
    if (player === undefined) return "loading";

    setPlaying(false);

    return "ok";
  };

  return {
    play,
    pause,
    loading,
    playing,
  };
};
