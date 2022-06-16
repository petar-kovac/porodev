import axios from 'axios';
import PCard from 'components/card/PCard';
import { FC, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';

import { Button, DatePicker, Modal, Select, Slider } from 'antd';
import { AppstoreOutlined, BarsOutlined } from '@ant-design/icons';

import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import ListCard from 'components/card/ListCard';
import PFolders from 'components/folders/PFolders';

const { RangePicker } = DatePicker;
const { Option } = Select;

interface IModalData {}

const Files: FC = () => {
  const [data, setData] = useState<[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [cardData, setCardData] = useState<any>();
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [isList, setIsList] = useState<boolean>(false);

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

  const handleChange = (value: string) => {
    console.log(`selected ${value}`);
  };

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        const res = await axios.get(`${process.env.REACT_APP_MOCK_URL}/files`);
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
        <StyledStaticContent>
          <StyledFoldersContainer>
            {data?.slice(0, 4).map((value: any) => (
              <PFolders
                heading={value?.name}
                description={value?.description}
              />
            ))}
          </StyledFoldersContainer>
          <StyledFilesHeader>
            <StyledFilesDateFilter>
              <h4>Filter files by date:</h4>
              <RangePicker />
            </StyledFilesDateFilter>

            <StyledFilesSlider>
              <h4>Filter by size:</h4>
              <Slider range defaultValue={[0, 30]} />
            </StyledFilesSlider>
            <StyledFilesSelect>
              <Select defaultValue="Filter by type" onChange={handleChange}>
                <Option value="jpg">.jpg</Option>
                <Option value="png">.png</Option>
                <Option value="txt">.txt</Option>
                <Option value="pdf">.pdf</Option>
              </Select>
              <Select defaultValue="Sort by" onChange={handleChange}>
                <Option value="asc">Ascending</Option>
                <Option value="desc">Descending</Option>
                <Option value="newest" disabled>
                  Newest
                </Option>
                <Option value="oldest">Oldest</Option>
              </Select>
              <StyledToggleIcons>
                {isList ? (
                  <StyledAppstoreOutlined onClick={() => setIsList(!isList)} />
                ) : (
                  <StyledBarsOutlined onClick={() => setIsList(!isList)} />
                )}
              </StyledToggleIcons>
            </StyledFilesSelect>
          </StyledFilesHeader>
          <StyledFilesWrapper>
            {data?.map((value: any, index: number) => (
              <>
                {isList ? (
                  <ListCard
                    image={value?.image}
                    heading={value?.name}
                    description={value?.description}
                    onClick={() => onClick(value)}
                  />
                ) : (
                  <PCard
                    image={value?.image}
                    heading={value?.name}
                    description={value?.description}
                    onClick={() => onClick(value)}
                  />
                )}
              </>
            ))}
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PFileSider
          title={cardData?.name}
          content={cardData?.description}
          isSiderVisible={isSiderVisible}
          setIsSiderVisible={setIsSiderVisible}
        />
      </StyledContent>

      <PModal
        isModalVisible={isModalVisible}
        title={cardData?.name}
        content={cardData?.description}
        setIsModalVisible={setIsModalVisible}
      />
    </StyledPageWrapper>
  );
};

const StyledPageWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const StyledContent = styled.div`
  display: flex;
`;

const StyledStaticContent = styled.div`
  display: flex;
  flex-direction: column;
  width: 80%;
  margin: 0 auto;
  align-items: flex-start;
  gap: 5rem;
`;

const StyledFoldersContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  margin-top: 3rem;
  gap: 2rem;
`;

const StyledFilesHeader = styled.div`
  width: 100%;
  display: flex;
  align-items: flex-end;
  gap: 1rem;
  justify-content: space-around;
  flex-wrap: wrap;
  padding: 2rem 0;
  background-color: #fcfcfc;
  border-radius: 3rem;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  border-bottom: 2px solid #ddd;
`;

const StyledToggleIcons = styled.div`
  margin-left: auto;
`;

const StyledFilesWrapper = styled.div`
  padding: 0 2rem;
  display: flex;
  justify-content: center;
  gap: 2.5rem;
  flex-wrap: wrap;
`;

const StyledFilesSlider = styled.div`
  width: 32rem;
  .ant-slider {
    margin: 2.2rem 0.6rem 1rem;
    padding: 0 !important;
  }
`;

const StyledFilesDateFilter = styled.div`
  width: 32rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  .ant-picker {
    border-radius: 1rem;
  }
`;

const StyledFilesSelect = styled.div`
  width: 32rem;
  align-self: flex-end;
  display: flex;
  gap: 1rem;
  align-items: center;

  .ant-select-selector {
    border-radius: 10px !important;
    color: #999;
  }
`;

const StyledAppstoreOutlined = styled(AppstoreOutlined)`
  font-size: 2rem;
  cursor: pointer;
  color: #555;
`;

const StyledBarsOutlined = styled(BarsOutlined)`
  font-size: 2rem;
  cursor: pointer;
  color: #555;
`;

export default Files;
