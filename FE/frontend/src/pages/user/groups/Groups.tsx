import axios from 'axios';
import PCard from 'components/card/PCard';
import { FC, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';

import { Button, DatePicker, Modal, Select, Slider } from 'antd';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import GroupCard from 'components/card/GroupCard';
import { PAGES } from 'util/constants/constants';
import PButton from 'components/buttons/PButton';
import theme from 'theme/theme';

const { RangePicker } = DatePicker;
const { Option } = Select;

interface IModalData {}

const Groups: FC = () => {
  const [data, setData] = useState<[]>([]);
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [cardData, setCardData] = useState<any>();

  const count = useRef(0);

  const onClick = useCallback((value: any) => {
    count.current += 1;
    setCardData(value);
    setTimeout(() => {
      if (count.current === 1) {
        setIsSiderVisible(true);
      }

      if (count.current === 2) {
        setIsSiderVisible(false);
        setIsModalVisible(true);
      }

      count.current = 0;
    }, 300);
  }, []);

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        const res = await axios.get(`${process.env.REACT_APP_MOCK_URL}/groups`);
        setData(res.data);
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

  return (
    <StyledPageWrapper>
      <StyledContent>
        <div style={{ display: 'flex', flexDirection: 'column' }}>
          <StyledHeadingWrapper>
            <StyledHeading>{PAGES.userGroups}:</StyledHeading>
            <PButton
              text="Create group"
              color="#fff"
              borderRadius="8px"
              htmlType="submit"
              form="loginForm"
              background="#2b1553"
            />
          </StyledHeadingWrapper>
          <StyledFilesWrapper>
            {data?.map((value: any, index: number) => (
              <GroupCard
                groupName={value.groupName}
                isModerator={value.isModerator}
                moderatorName={value.moderatorName}
                numberOfFiles={value.numberOfFiles}
                numberOfUsers={value.numberOfUsers}
                onClick={() => onClick(value)}
                uuid={value.uuid}
              />
            ))}
          </StyledFilesWrapper>
        </div>

        <PFileSider
          title={cardData?.groupName}
          content={cardData?.moderatorName}
          isSiderVisible={isSiderVisible}
          setIsSiderVisible={setIsSiderVisible}
        />
      </StyledContent>
      <PModal
        title={cardData?.name}
        content={cardData?.description}
        isModalVisible={isModalVisible}
        setIsModalVisible={setIsModalVisible}
      />
    </StyledPageWrapper>
  );
};
export const StyledHeadingWrapper = styled.div`
  display: flex;
  padding: 20px;
  align-items: center;
  gap: 10px;
  /* justify-content: space-between; */
`;
export const StyledHeading = styled.div`
  font-size: 24px;
  color: ${({ theme: { colors } }) => colors.primary};
  font-weight: 600;
`;
const StyledContent = styled.div`
  display: flex;
`;
const StyledPageWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const StyledFilesWrapper = styled.div`
  padding: 20px;
  display: flex;
  justify-content: center;
  gap: 25px;
  flex-wrap: wrap;
`;

export default Groups;
