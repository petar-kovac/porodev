import axios from 'axios';
import { useState, useEffect } from 'react';
import { IFilesCard } from 'types/card-data';

export const useFetchData = (url: string) => {
  const [data, setData] = useState<IFilesCard[] | null>(null);

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        const res = await axios.get(url);
        setData(res.data);
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, [url]);

  return { data };
};
