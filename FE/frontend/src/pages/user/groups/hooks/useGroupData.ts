import { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import { findFiles } from 'service/files/files';
import { usePageContext } from 'context/PageContext';
import { getAllSharedSpaces } from 'service/shared-spaces/shared-spaces';

const useGroupData = () => {
  const [isLoading, setisLoading] = useState<boolean>(false);
  const [data, setData] = useState<[] | undefined>(undefined);
  const [error, setError] = useState<string>('');

  const { setIsSiderVisible, isSiderVisible, sharedSpaceId } = usePageContext();

  console.log(data, 'datad');

  useEffect(() => {
    setIsSiderVisible(false);
    const fetchFiles = async () => {
      try {
        const res = await getAllSharedSpaces();
        setData(res);
      } catch (err: any) {
        setError(err.message);
      }
    };
    fetchFiles();
  }, [sharedSpaceId]);

  return {
    isLoading,
    data,
    error,
    setData,
    setError,
  };
};

export default useGroupData;
