import { FC, useEffect } from 'react';
import { findFiles } from '../../service/files/files';

const Files: FC = () => {
  useEffect(() => {
    const fetchFiles = () => {
      try {
        const res = findFiles();
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

  return <div>Files</div>;
};

export default Files;
