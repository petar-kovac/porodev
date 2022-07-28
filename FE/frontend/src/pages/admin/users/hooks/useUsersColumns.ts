import { useMemo } from 'react';
import { formatDate } from 'util/helpers/date-formaters';

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
        title: 'department',
        dataIndex: 'department',
        key: 'department',
      },
      {
        title: 'verifiedAt',
        dataIndex: 'verifiedAt',
        key: 'verifiedAt',
        render: (record: any) => {
          return formatDate(record);
        },
      },
      {
        title: 'runtimeTotal',
        dataIndex: 'runtimeTotal',
        key: 'runtimeTotal',
      },
    ],
    [],
  );

  return { columns };
};

export default useUsersColumns;
