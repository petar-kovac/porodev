import { FC, useEffect } from 'react';

import PButton from 'components/buttons/PButton';
import PTable from 'components/table/PTable';
import {
  StyledHeading,
  StyledHeadingWrapper,
  StyledPage,
} from 'styles/commonStyles';
import { PAGES } from 'util/constants/constants';

import useAdminsColumns from './hooks/useAdminsColumns';
import useAdminsData from './hooks/useAdminsData';

const Admins: FC = () => {
  const { columns } = useAdminsColumns();
  const { isLoading, data, error, findData } = useAdminsData();

  useEffect(() => {
    findData();
  }, []);

  return (
    <StyledPage>
      <StyledHeadingWrapper>
        <StyledHeading>{PAGES.admins}</StyledHeading>
        <PButton
          text="Add admin"
          color="#000"
          borderRadius="12px"
          background="#fff"
        />
      </StyledHeadingWrapper>
      <PTable dataSource={data} columns={columns} />
    </StyledPage>
  );
};

export default Admins;
