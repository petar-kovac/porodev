import axios from 'axios';
import { FC, ReactNode, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';

import RuntimeCard from 'components/card/RuntimeCard';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { startRuntimeService } from 'service/runtime/runtime';
import { IFilesCard } from 'types/card-data';
import { GetRuntimeModalData } from 'util/util-components/GetRuntimeModalData';
import {
  StyledContent,
  StyledFilesWrapper,
  StyledPageWrapper,
  StyledStaticContent,
} from './runtime-styled';

const Runtime: FC = () => {
  const [data, setData] = useState<IFilesCard[] | undefined>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | undefined>(undefined);
  const [content, setContent] = useState<ReactNode>(undefined);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const startRuntime = async () => {
    setIsLoading(true);

    const res = await startRuntimeService({
      fileID: 'b22a0496-ded5-4fe5-bbb3-e20280b70a03',
      jwt: localStorage.getItem('accessToken'),
    });
    const modalDataToRender: ReactNode = GetRuntimeModalData(res);

    setContent(modalDataToRender);
    setIsSiderVisible(false);
    setIsModalVisible(true);
    setIsLoading(false);
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
        <StyledStaticContent onClick={() => setIsSiderVisible(false)}>
          <StyledFilesWrapper>
            {data?.slice(0, 3).map((value: any, index) => (
              <>
                <RuntimeCard
                  key={value.id}
                  keyless={value?.id}
                  title={value?.title}
                  selected={value?.id === cardData?.id}
                  createdAt={value?.createdAt}
                  onClick={(e) => {
                    e.stopPropagation();
                    setCardData(value);
                    setIsSiderVisible(true);
                  }}
                  onDoubleClick={(e) => {
                    e.stopPropagation();
                    setCardData(value);
                    setIsSiderVisible(false);
                    setIsModalVisible(true);
                  }}
                />
              </>
            ))}
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PFileSider
          cardData={cardData}
          isSiderVisible={isSiderVisible}
          setIsSiderVisible={setIsSiderVisible}
          type="runtime"
          onButtonClick={startRuntime}
          isLoading={isLoading}
        />
      </StyledContent>
      <PModal
        title="Result of you action: "
        isModalVisible={isModalVisible}
        cardData={cardData}
        setIsModalVisible={setIsModalVisible}
        content={content}
      />
    </StyledPageWrapper>
  );
};

export default Runtime;
