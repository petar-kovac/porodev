import { FC, useEffect } from 'react';

import PButton from 'components/buttons/Button';
import PTable from 'components/table/PTable';
import {
  StyledHeading,
  StyledHeadingWrapper,
  StyledPage,
} from 'styles/commonStyles';
import { PAGES } from 'util/constants/constants';

import useUsersColumns from './hooks/useUsersColumns';
import useUsersData from './hooks/useUsersData';

const Users: FC = () => {
  const { columns } = useUsersColumns();
  const { isLoading, data, error, findData } = useUsersData();

  useEffect(() => {
    findData();
  }, []);

  return (
    <StyledPage>
      <StyledHeadingWrapper>
        <StyledHeading>{PAGES.users}</StyledHeading>
        <PButton
          text="Add user"
          color="#000"
          borderRadius="12px"
          background="#fff"
        />
      </StyledHeadingWrapper>
      <PTable dataSource={data} columns={columns} />
    </StyledPage>
  );
};

export default Users;
