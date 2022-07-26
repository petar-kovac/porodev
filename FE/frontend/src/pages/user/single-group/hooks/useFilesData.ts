import { usePageContext } from 'context/PageContext';
import { useEffect, useState } from 'react';
import { findFiles } from 'service/files/files';
import { getAllFiles } from 'service/shared-spaces/shared-spaces';

const useFilesData = (id: string) => {
  const [isLoading, setisLoading] = useState<boolean>(false);
  const [data, setData] = useState<[] | undefined>(undefined);
  const [error, setError] = useState<string>('');

  const { setIsSiderVisible } = usePageContext();
  console.log(id, 'id');

  useEffect(() => {
    setIsSiderVisible(false);
    const fetchFiles = async () => {
      setisLoading(true);
      try {
        const res = await getAllFiles(id);
        setData(res);
      } catch (err: any) {
        setError(err.message);
      } finally {
        setisLoading(false);
      }
    };
    fetchFiles();
  }, []);

  return {
    isLoading,
    data,
    error,
    setData,
    setError,
  };
};

export default useFilesData;
