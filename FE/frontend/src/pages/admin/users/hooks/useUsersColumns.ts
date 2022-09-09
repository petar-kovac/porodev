import { useMemo } from 'react';
import { formatDate } from 'util/helpers/date-formaters';
import { findPosition } from 'util/helpers/find-user-position';

const useUsersColumns = () => {
  const columns = useMemo(
    () => [
      {
        title: 'Name',
        dataIndex: 'name',
        key: 'name',
      },
      {
        title: 'Last name',
        dataIndex: 'lastname',
        key: 'lastname',
      },
      {
        title: 'Email',
        dataIndex: 'email',
        key: 'email',
      },
      {
        title: 'Department',
        dataIndex: 'department',
        key: 'department',
        render: (record: any) => {
          return findPosition(record);
        },
      },
      {
        title: 'Verified at',
        dataIndex: 'verifiedAt',
        key: 'verifiedAt',
        render: (record: any) => {
          return formatDate(record);
        },
      },
      {
        title: 'Total runtime',
        dataIndex: 'runtimeTotal',
        key: 'runtimeTotal',
      },
      {
        title: 'Total upload',
        dataIndex: 'fileUploadTotal',
        key: 'fileUploadTotal',
        render: (record: any) => {
          return `${Number(record / 1000000).toFixed(2)} MB`;
        },
      },
      {
        title: 'Total download',
        dataIndex: 'fileDownloadTotal',
        key: 'fileDownloadTotal',
        render: (record: any) => {
          return `${Number(record / 1000000).toFixed(2)} MB`;
        },
      },
    ],
    [],
  );

  return { columns };
};

export default useUsersColumns;
